using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.IO;

namespace FarcardContract
{
    public class FarcardsAllFabric
    {
        [Import(typeof(IFarcards))]
#pragma warning disable 0649
        private IFarcards _processor;
#pragma warning restore 0649
        public IFarcards GetProcessor(FileInfo path = null)
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
