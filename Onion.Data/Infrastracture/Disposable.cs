using System;
using System.Collections.Generic;
using System.Text;

namespace Onion.Data.Infrastracture
{
    public class Disposable : IDisposable
    {
        private bool isDisposed;
               
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!isDisposed && disposing)
            {
                DisposeCore();
            }

            isDisposed = true;
        }

        /// <summary>
        /// Override this to dispose custom objects.
        /// </summary>
        protected virtual void DisposeCore()
        {
        }
    }
}
