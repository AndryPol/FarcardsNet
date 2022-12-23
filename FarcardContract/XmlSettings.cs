using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace FarcardContract
{
    [Serializable]
    public class XmlSettings<T>
    {
        Logger<XmlSettings<T>> Log = new Logger<XmlSettings<T>>();

        protected XmlSettings(bool deserialize)
        {
            if (deserialize)
                Deserialize();
        }

        string _path;
        public string SettingsPath{ get => _path; set => _path = value; }

        static string GetSettingsPath<M>()
        {
            Type tm = typeof(M);
            string settingPath = Path.Combine(Path.GetDirectoryName(tm.Assembly.Location),
           tm.Name + ".config"
           );
            return settingPath;
        }

        public static T GetSettings(string path = null)
        {
            var sett = DeserializeSettings<T>(path);
            return sett;
        }



        [XmlElement("Settings")]
        public T ObjectSettings { get; set; } = default(T);
        protected XmlSettings()
        {

        }

        public void Save()
        {

            serialize();
        }

        void serialize()
        {
            try
            {
                if (ObjectSettings == null)
                    ObjectSettings = Activator.CreateInstance<T>();
                Log.Debug("записываем настройки");
                XmlSerializer form = new XmlSerializer(this.GetType());

                if (string.IsNullOrWhiteSpace(_path))
                    _path = GetSettingsPath<T>();
                
                using (var fs = new FileStream(_path, FileMode.Create, FileAccess.ReadWrite))
                {
                    XmlSerializerNamespaces namespaces = new XmlSerializerNamespaces();
                    namespaces.Add(string.Empty, string.Empty);

                    form.Serialize(fs, this, namespaces);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex.ToString());
            }
            Log.Debug("настройки записаны");
        }

        public void Deserialize()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(_path))
                    _path = GetSettingsPath<T>();
                ObjectSettings = Activator.CreateInstance<T>();
                Log.Debug("читаем настройки FarcardNet");
                XmlSerializer form = new XmlSerializer(this.GetType());

                using (var fs = new FileStream(_path, FileMode.Open, FileAccess.Read))
                {
                    var s = (XmlSettings<T>)form.Deserialize(fs);
                    var pr = this.GetType().GetProperties();
                    var prs = s.GetType();
                    foreach (var p in pr)
                    {
                        var val = prs.GetProperty(p.Name).GetValue(s, null);


                        p.SetValue(this, val, null);
                        String valst = "";

                        valst = val?.ToString();
                        Log.Debug(string.Format("{0}:{1}", p.Name, valst));
                    }
                    ObjectSettings = s.ObjectSettings;
                }
            }
            catch (FileNotFoundException)
            {

                Save();
                Deserialize();
            }
            catch (Exception ex)
            {
                Log.Error(string.Format("Ошибка чтения настроек: {0}", ex.ToString()));
            }
            Log.Debug("настройки считаны");

        }

        static N DeserializeSettings<N>(string path = null)
        {
            Logger<XmlSettings<N>> Log = new Logger<XmlSettings<N>>();
            var m = Activator.CreateInstance<N>();
            var s = new XmlSettings<N>();
            s.ObjectSettings = m;
            var settingsPath = path;
            if (string.IsNullOrWhiteSpace(path))
                settingsPath = GetSettingsPath<T>();
            try
            {
                Log.Debug("читаем настройки");
                XmlSerializer form = new XmlSerializer(s.GetType());
                
                using (var fs = new FileStream(settingsPath, FileMode.Open, FileAccess.Read))
                {
                    var temp = form.Deserialize(fs) as XmlSettings<N>;
                    if (temp != null)
                        m = temp.ObjectSettings;

                    Log.Debug("настройки считаны");

                }
            }
            catch (FileNotFoundException)
            { 
                s._path = settingsPath;
                s.Save();
                m = DeserializeSettings<N>(settingsPath);
            }
            catch (Exception ex)
            {

                Log.Error(string.Format("Ошибка чтения настроек: {0}", ex.ToString()));
            }

            return m;
        }
    }
}
