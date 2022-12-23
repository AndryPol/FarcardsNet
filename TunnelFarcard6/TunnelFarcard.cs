using FarcardContract;
using FarcardContract.Farcard6;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace TunnelFarcard6
{
    [Export(typeof(IFarcards6))]
    public class TunnelFarcard : IFarcards6, IDisposable
    {
        readonly Logger<TunnelFarcard> _logger = new Logger<TunnelFarcard>();
        readonly TunnelFarcardSettings _settings = TunnelFarcardSettings.GetSettings();

        IntPtr hModule;

        Init init;
        Done done;
        GetCardInfoEx cardInfoEx;
        TransactionsEx transactionsEx;
        public void Init_dll()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_settings.PathDll))
                    throw new ArgumentNullException(nameof(_settings.PathDll), "Не задан путь к библиотеке");
                hModule = NativeWin32.LoadLibrary(_settings.PathDll);
                if (hModule == IntPtr.Zero)
                {
                    var err = NativeWin32.GetLastError();
                    throw new NullReferenceException($"Не удалось загрузить библиотеку{_settings.PathDll} errorCode: {err}");
                }

                cardInfoEx = NativeWin32.GetDelegate<GetCardInfoEx>(hModule,nameof(GetCardInfoEx),true);
                transactionsEx = NativeWin32.GetDelegate<TransactionsEx>(hModule, nameof(TransactionsEx), true);
                init = NativeWin32.GetDelegate<Init>(hModule, nameof(Init));
                done = NativeWin32.GetDelegate<Done>(hModule, nameof(Done));
                if (init != null)
                    init();
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
                Dispose();
                throw ex;
            }
        }

        public void FindCardsL(string FindText, CBFind CbFind, IntPtr Back)
        {
            throw new NotImplementedException();
        }

        public int FindEmail_dll(string Email, ref HolderInfo OutInfo)
        {
            throw new NotImplementedException();
        }

        public int GetCardInfoEx_dll(long Card, uint Restaurant, uint UnitNo, ref CardInfoEx CardInfo, byte[] InpBuf, ushort InpKind, out string OutBuf, out ushort OutKind)
        {
            int res = 1;
            OutBuf = null;
            OutKind = 0;
            try
            {
                int outlen = 0;
                if (InpBuf == null)
                    InpBuf = new byte[]{};
                byte[] outbuf;
                if (cardInfoEx != null)
                    res = cardInfoEx(Card, Restaurant, UnitNo,ref CardInfo,InpBuf,(uint)InpBuf.Length,InpKind,out outbuf,out outlen, out OutKind);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
            

            return res;
        }

        public int TransactionsEx_dll(List<TransactionInfoEx> TransactionInfo, byte[] InpBuf, ushort InpKind, out string OutBuf, out ushort OutKind)
        {
            int res = 1;
            OutBuf = null;
            OutKind = 0;
            GCHandle handle = new GCHandle();
            try
            {
                handle = GCHandle.Alloc(InpBuf, GCHandleType.Pinned);
                int outlen = 0;
                if (transactionsEx != null)
                    res = transactionsEx((UInt32)TransactionInfo.Count(), IntPtr.Zero, handle.AddrOfPinnedObject(), (UInt32)InpBuf.Length, InpKind, out OutBuf, out outlen, out OutKind);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
            finally
            {
                if (handle.IsAllocated)
                    handle.Free();
            }

            return res;
        }
        public void Done_dll()
        {
            try
            {
                done?.Invoke();
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
                if (init != null)
                    init = null;
                if (done != null)
                    done = null;
                if (cardInfoEx != null)
                    cardInfoEx = null;
                if (transactionsEx != null)
                    transactionsEx = null;
                if (hModule != null)
                    NativeWin32.FreeLibrary(hModule);
                hModule = IntPtr.Zero;
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
        }

        
    }
}
