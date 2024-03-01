using FarcardContract.Farcard6;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Hosting;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Text;

namespace FarcardContract.Farcard5
{
    public class Farcards5Factory
    {
        [Import(typeof(IFarcards5))]
#pragma warning disable 0649
        private IFarcards5 _processor;
#pragma warning restore 0649
        public IFarcards5 GetProcessor(FileInfo path = null)
        {

            var catalog = new AggregateCatalog();
            if (path == null)
                catalog.Catalogs.Add(new AssemblyCatalog(GetType().Assembly));
            else
            {
                if (!path.Exists)
                {
                    throw new FileNotFoundException(path.FullName);
                }

                catalog.Catalogs.Add(new AssemblyCatalog(path.FullName));
            }

            var container = new CompositionContainer(catalog);
            container.ComposeParts(this);

            return _processor;
        }
    }
}
