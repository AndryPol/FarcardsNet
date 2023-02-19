using FarcardContract;
using FarcardContract.Data;
using FarcardContract.Data.Farcard6;
using FarcardContract.Farcard6;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;


namespace TunnelFarcard6
{
    [Export(typeof(IFarcards6))]
    public class TunnelFarcard : IFarcards6,
        IDisposable
    {
        private readonly TunnelFarcardSettings _settings;

        private readonly Logger<TunnelFarcard> _logger;


        private IntPtr hModule;

        private Init _init;
        private Done _done;
        private GetCardInfoEx _getCardInfoEx;
        private TransactionsEx _transactionsEx;
        private GetCardImageEx _getCardImageEx;
        private FindEmail _findEmail;
        private FindCardsL _findCardsL;
        private FindAccountsByKind _findAccountsByKind;
        private GetDiscLevelInfoL _getDiscLevelInfoL;
        private AnyInfo _anyInfo;

        private string _moduleName;

        private string _pathModule;

        public TunnelFarcard()
        {
            _settings = TunnelFarcardSettings.GetSettings();
            _logger = new Logger<TunnelFarcard>(_settings.LogLevel);
            _logger.GetLogger().Info($"Start TunnelFarcard: version {GetType().Assembly.GetName().Version}");
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            try
            {
                _moduleName = _settings.PathDll;
                if (string.IsNullOrWhiteSpace(_moduleName))
                    throw new ArgumentNullException(nameof(_settings.PathDll), "Не задан путь к библиотеке");

                var finfo = new FileInfo(_moduleName);
                _pathModule = finfo.FullName;
                hModule = NativeWin32.LoadLibrary(finfo.FullName);
                if (hModule == IntPtr.Zero)
                {
                    var err = NativeWin32.GetLastError();
                    var ex = new Win32Exception(err);
                    throw new NullReferenceException($"Не удалось загрузить библиотеку {_settings.PathDll} errorCode: {err} {ex.Message}");
                }

                _getCardInfoEx = NativeWin32.GetDelegate<GetCardInfoEx>(hModule, nameof(FarcardContract.GetCardInfoEx), true);
                _transactionsEx = NativeWin32.GetDelegate<TransactionsEx>(hModule, nameof(FarcardContract.TransactionsEx), true);
                _init = NativeWin32.GetDelegate<Init>(hModule, nameof(FarcardContract.Init));
                _done = NativeWin32.GetDelegate<Done>(hModule, nameof(FarcardContract.Done));
                _getCardImageEx = NativeWin32.GetDelegate<GetCardImageEx>(hModule, nameof(FarcardContract.GetCardImageEx));
                _findCardsL = NativeWin32.GetDelegate<FindCardsL>(hModule, nameof(FarcardContract.FindCardsL));
                _findEmail = NativeWin32.GetDelegate<FindEmail>(hModule, nameof(FarcardContract.FindEmail));
                _findAccountsByKind = NativeWin32.GetDelegate<FindAccountsByKind>(hModule, nameof(FarcardContract.FindAccountsByKind));
                _getDiscLevelInfoL = NativeWin32.GetDelegate<GetDiscLevelInfoL>(hModule, nameof(FarcardContract.GetDiscLevelInfoL));
                _anyInfo = NativeWin32.GetDelegate<AnyInfo>(hModule, nameof(FarcardContract.AnyInfo));

                var app = Process.GetCurrentProcess();

                _logger.Trace($"start app {app.Id} : {app.ProcessName}");

                foreach (ProcessModule module in app.Modules)
                {
                    _logger.Trace($"load module ;{module.ModuleName};");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                Dispose();
                throw ex;
            }
        }


        public void Init()
        {
            try
            {
                if (_init != null)
                {
                    _init();
                    var err = NativeWin32.GetLastError();
                    if (err != 0)
                        throw new Win32Exception(err);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }

        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.Error($"unhandledExc : {(e.ExceptionObject as Exception)}");
        }

        public void FindCardsL(string findText, CBFind cbFind, IntPtr backPtr)
        {
            try
            {
                if (_findCardsL != null)
                    _findCardsL(findText, cbFind, backPtr);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }

        public void FindAccountsByKind(FindKind kind, string findText, CBFind cbFind, IntPtr backPtr)
        {
            try
            {
                if (_findAccountsByKind != null)
                    _findAccountsByKind(kind, findText, cbFind, backPtr);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }

        public void AnyInfo(byte[] inpBuf, out byte[] outBuf)
        {
            outBuf = null;
            try
            {
                IntPtr pOutBuf = IntPtr.Zero;
                UInt32 outLen = 0;
                if (inpBuf == null)
                    inpBuf = new byte[0];

                if (_anyInfo != null)
                    _anyInfo(inpBuf, (UInt32)inpBuf.Length, out pOutBuf, out outLen);

                if (pOutBuf != IntPtr.Zero && outLen > 0)
                {
                    var ansiBuf = new byte[outLen];
                    Marshal.Copy(pOutBuf, ansiBuf, 0, (int)outLen);

                    if (ansiBuf.Count(x => x.Equals(0)) <= 1)
                    {
                        outBuf = ansiBuf;
                    }
                    else
                    {
                        var charArray = new char[outLen];

                        Marshal.Copy(pOutBuf, charArray, 0, (int)outLen);
                        outBuf = Encoding.UTF8.GetBytes(new string(charArray));
                    }
                }

            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }

        public int GetDiscLevelInfoL(uint account, ref DiscLevelInfo info)
        {
            int res = 1;
            try
            {
                if (_getDiscLevelInfoL != null)
                    res = _getDiscLevelInfoL(account, info);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
            return res;
        }

        public int GetCardImageEx(long card, ref TextInfo info)
        {
            int res = 1;
            try
            {
                if (_getCardImageEx != null)
                    res = _getCardImageEx(card, info);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }

            return res;
        }

        public int FindEmail(string email, ref HolderInfo holderInfo)
        {
            int res = 1;
            try
            {
                if (_findEmail != null)
                    return _findEmail(email, holderInfo);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }

            return res;
        }

        public int GetCardInfoEx(long card, uint restaurant, uint unitNo, ref CardInfoEx cardInfo, byte[] inpBuf, BuffKind inpKind, out byte[] outBuf, out BuffKind outKind)
        {
            int res = 1;
            outBuf = null;
            outKind = 0;

            IntPtr pOutBuf = IntPtr.Zero;
            UInt32 outLen = 0;
            BuffKind _outKind = BuffKind.None;
            try
            {
                if (inpBuf == null)
                    inpBuf = new byte[] { };

                if (_getCardInfoEx != null)
                {
                    res = _getCardInfoEx(card,
                        restaurant,
                        unitNo,
                        cardInfo,
                        inpBuf,
                        (UInt32)inpBuf.Length,
                        inpKind,
                        out pOutBuf,
                        out outLen,
                        out _outKind);
                }

                if (_outKind != BuffKind.None && pOutBuf != IntPtr.Zero && outLen > 0)
                {
                    var ansiBuf = new byte[outLen];
                    Marshal.Copy(pOutBuf, ansiBuf, 0, (int)outLen);

                    if (ansiBuf.Count(x => x.Equals(0)) <= 1)
                    {
                        outBuf = ansiBuf;
                    }
                    else
                    {
                        var charArray = new char[outLen];
                        Marshal.Copy(pOutBuf, charArray, 0, (int)outLen);
                        outBuf = Encoding.UTF8.GetBytes(new string(charArray));
                    }
                    outKind = _outKind;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
            catch
            {
                var err = NativeWin32.GetLastError();
                _logger.Error(new Win32Exception(err).Message);
            }

            return res;
        }

        public int TransactionsEx(List<TransactionInfoEx> transactionInfo, byte[] inpBuf, BuffKind inpKind, out byte[] outBuf, out BuffKind outKind)
        {
            int res = 1;
            outBuf = null;
            outKind = 0;

            IntPtr pOutBuf = IntPtr.Zero;
            UInt32 outLen = 0;
            BuffKind _outKind = BuffKind.None;

            var listIntPtrs = new List<IntPtr>();
            IntPtr nativeArray = IntPtr.Zero;

            try
            {
                if (inpBuf == null)
                    inpBuf = new byte[] { };

                int intPtrSize = IntPtr.Size;

                nativeArray = Marshal.AllocCoTaskMem(intPtrSize * transactionInfo.Count);
                var transactionSize = Marshal.SizeOf(typeof(TransactionInfoEx));
                for (int i = 0; i < transactionInfo.Count; i++)
                {
                    IntPtr nativeTransact = Marshal.AllocCoTaskMem(transactionSize);
                    listIntPtrs.Add(nativeTransact);
                    Marshal.StructureToPtr(transactionInfo[i], nativeTransact, true);

                    Marshal.WriteIntPtr(nativeArray, i * intPtrSize, nativeTransact);
                }



                if (_transactionsEx != null)
                {
                    res = _transactionsEx((uint)listIntPtrs.Count,
                        nativeArray,
                        inpBuf,
                        (UInt32)inpBuf.Length,
                        inpKind,
                        out pOutBuf,
                        out outLen,
                        out _outKind);
                }

                if (_outKind != BuffKind.None && pOutBuf != IntPtr.Zero && outLen > 0)
                {
                    var ansiBuf = new byte[outLen];
                    Marshal.Copy(pOutBuf, ansiBuf, 0, (int)outLen);

                    if (ansiBuf.Count(x => x.Equals(0)) <= 1)
                    {
                        outBuf = ansiBuf;
                    }
                    else
                    {
                        var charArray = new char[outLen];
                        Marshal.Copy(pOutBuf, charArray, 0, (int)outLen);
                        outBuf = Encoding.UTF8.GetBytes(new string(charArray));
                    }
                    outKind = _outKind;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
            catch
            {
                var err = NativeWin32.GetLastError();
                _logger.Error(new Win32Exception(err).Message);
            }
            finally
            {
                if (nativeArray != IntPtr.Zero)
                {
                    Marshal.FreeCoTaskMem(nativeArray);
                    nativeArray = IntPtr.Zero;
                }

                for (int i = 0; i < listIntPtrs.Count; i++)
                {
                    IntPtr ptr = listIntPtrs[i];
                    if (ptr != IntPtr.Zero)
                    {
                        Marshal.DestroyStructure(ptr, typeof(TransactionInfoEx));
                        Marshal.FreeCoTaskMem(ptr);
                        ptr = IntPtr.Zero;
                    }
                }
            }

            return res;
        }

        public void Done()
        {
            try
            {
                if (_done != null)
                {
                    _done();
                    var err = Marshal.GetLastWin32Error();
                    if (err != 0)
                        throw new Win32Exception(err);
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }

        public void Dispose()
        {
            try
            {
                Done();

                _init = null;
                _done = null;
                _getCardInfoEx = null;
                _transactionsEx = null;
                _findEmail = null;
                _findCardsL = null;
                _findAccountsByKind = null;
                _getCardImageEx = null;
                _getDiscLevelInfoL = null;
                _anyInfo = null;

                if (hModule != IntPtr.Zero)
                    NativeWin32.FreeLibrary(hModule);
                hModule = IntPtr.Zero;

                AppDomain.CurrentDomain.UnhandledException -= CurrentDomain_UnhandledException;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }
    }
}
