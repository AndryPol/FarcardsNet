using FarcardContract.Data;
using FarcardContract.Data.Farcard6;
using FarcardContract.Farcard5;
using FarcardContract.Farcard6;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using FarcardContract.Demo.Farcard6;

namespace FarcardContract
{
    [Export(typeof(IFarcards))]
    internal class FarcardAllDemo : IFarcards
    {
        private readonly Farcard6Demo _farcard6Demo = new Farcard6Demo();
        private readonly Farcard5Demo _farcard5Demo = new Farcard5Demo();
        private Logger<FarcardAllDemo> _logger = new Logger<FarcardAllDemo>();
        public FarcardAllDemo()
        {

        }

        public void Init()
        {
            try
            {
                _farcard5Demo.Init();
            }
            catch (Exception ex)
            {

                _logger.Error(ex);
            }
            try
            {
                _farcard6Demo.Init();
            }
            catch (Exception ex)
            {

                _logger.Error(ex);
            }
        }

        public int GetCardInfoEx(long card, uint restaurant, uint unitNo, ref CardInfoEx cardInfo, Byte[] inpBuf, BuffKind inpKind, out byte[] outBuf, out BuffKind outKind)
        {
            outBuf = null;
            outKind = 0;
            var res = 1;
            try
            {
                res = _farcard6Demo.GetCardInfoEx(card, restaurant, unitNo, ref cardInfo, inpBuf, inpKind,
                    out outBuf, out outKind);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return res;
        }

        public int TransactionsEx(List<TransactionInfoEx> transactionInfo, Byte[] inpBuf, BuffKind inpKind, out byte[] outBuf, out BuffKind outKind)
        {

            outBuf = null;
            outKind = 0;
            var res = 1;
            try
            {
                res = _farcard6Demo.TransactionsEx(transactionInfo, inpBuf, inpKind, out outBuf, out outKind);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return res;
        }

        public int FindEmail(string email, ref HolderInfo holderInfo)
        {
            var res = 1;
            try
            {
                res = _farcard6Demo.FindEmail(email, ref holderInfo);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return res;
        }

        public void FindCardsL(string findText, CBFind cbFind, IntPtr backPtr)
        {
            try
            {
                _farcard6Demo.FindCardsL(findText, cbFind, backPtr);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

        }

        public void FindAccountsByKind(FindKind kind, string findText, CBFind cbFind, IntPtr backPtr)
        {
            try
            {
                _farcard6Demo.FindAccountsByKind(kind, findText, cbFind, backPtr);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public void AnyInfo(byte[] inpBuf, out byte[] outBuf)
        {
            outBuf = null;
            try
            {
                _farcard6Demo.AnyInfo(inpBuf, out outBuf);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public int GetDiscLevelInfoL(uint account, ref DiscLevelInfo info)
        {
            try
            {
                return _farcard6Demo.GetDiscLevelInfoL(account, ref info);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }

            return 1;
        }

        public void Done()
        {
            try
            {
                _farcard5Demo.Done();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            try
            {
                _farcard6Demo.Done();
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
        }

        public int GetCardImageEx(long card, ref TextInfo info)
        {
            var res = 1;
            try
            {
                res = _farcard6Demo.GetCardImageEx(card, ref info);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
            }
            return res;
        }
    }
}
