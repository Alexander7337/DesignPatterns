namespace Prototype
{
    using Newtonsoft.Json;
    using System;
    using System.IO;
    using System.Runtime.Serialization.Formatters.Binary;
    using System.Xml.Serialization;

    [Serializable]
    public class Line
    {
        public Point Start, End;

        public override string ToString()
        {
            return $"{nameof(this.Start)}: {Start.X} - {Start.Y}; {nameof(this.End)}: {End.X} - {End.Y}";
        }
    }

    [Serializable]
    public class Point
    {
        public int X, Y;
    }

    public static class Extensions
    {
        public static T DeepCopy<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;

                return (T)s.Deserialize(ms);
            }
        }

        /// <summary>
        /// Deep Copy using JsonSerializer. Requires default controllers.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T DeepCopyJson<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                var json = JsonConvert.SerializeObject(self);

                return JsonConvert.DeserializeObject<T>(json);
            }
        }

        /// <summary>
        /// Deep Copy using Binary Formatter. Requires Serializable attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T DeepCopyBinaryFormatter<T>(this T self)
        {
            MemoryStream stream = new MemoryStream();
            BinaryFormatter formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T)copy;
        }
    }
}
