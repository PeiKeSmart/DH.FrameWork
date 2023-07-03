using System.Drawing;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Serialization;

namespace DH.Helpers;

public static class BitmapUtilities {
    public static Bitmap Concat(this Bitmap bitmap, Bitmap[] bitmaps, Size resultSize)
    {
        if (bitmap == null)
            throw new ArgumentNullException(nameof(bitmap));
        if (bitmaps == null)
            throw new ArgumentNullException(nameof(bitmaps));

        var totalWidth = bitmap.Width;
        var totalHeight = bitmap.Height;

        var lastWidth = 0;
        var lastHeight = 0;

        foreach (var b in bitmaps)
        {
            totalHeight += b.Height;
            totalWidth += b.Width;
        }

        using Graphics g = Graphics.FromImage(bitmap);
        foreach (var b in bitmaps)
        {
            g.DrawImage(b, lastWidth, lastHeight, 1920, 1080);

            lastWidth += b.Width;
            lastHeight += b.Height;
        }
        return bitmap.Resize(resultSize);
    }

    public static Bitmap Resize(this Bitmap bitmap, int width, int height)
    {
        return new Bitmap(bitmap, width, height);
    }
    public static Bitmap Resize(this Bitmap bitmap, double width, double height)
    {
        return new Bitmap(bitmap, width.ToInt(), height.ToInt());
    }
    public static Bitmap Resize(this Bitmap bitmap, Size newSize)
    {
        return new Bitmap(bitmap, newSize);
    }
    public static string GetImagePath(string name)
    {
        if (File.Exists(name + ".jpg"))
            return name + ".jpg";
        else if (File.Exists(name + ".jpeg"))
            return name + ".jpeg";
        else if (File.Exists(name + ".png"))
            return name + ".png";
        return string.Empty;
    }
}
public static class ArrayUtilities {
    public static bool IsNullOrEmpty(this Array array)
    {
        if (array == null || array.Length == 0)
            return true;
        else
            return false;
    }
}
public static class XmlUtilities {
    public static T FromXml<T>(this string xml)
        where T : class, new()
    {
        if (string.IsNullOrEmpty(xml))
            return null;
        using var memoryStream = new MemoryStream();
        memoryStream.Write(Encoding.UTF8.GetBytes(xml));
        memoryStream.Position = 0;
        var serializer = new XmlSerializer(typeof(T));
        return (T)serializer.Deserialize(memoryStream);
    }
    public static string ToXml<T>(this T obj)
    {
        using var memoryStream = new MemoryStream();
        var serializer = new XmlSerializer(typeof(T));
        serializer.Serialize(memoryStream, obj);
        return Encoding.UTF8.GetString(memoryStream.ToArray());
    }
}
public static class ConvertUtilities {
    public static object ParseTo(this string str, Type toType)
    {
        return Convert.ChangeType(str, toType, CultureInfo.InvariantCulture);
    }
    public static void Switch<T>(ref T obj1, ref T obj2)
    {
        T temp = obj1;
        obj1 = obj2;
        obj2 = temp;
    }

    public static Guid GenerateGuid(this string text)
    {
        using (var md5 = MD5.Create())
        {
            var hash = md5.ComputeHash(Encoding.Default.GetBytes(text));
            return new Guid(hash);
        }
    }

    public static int IndexOf<T>(this IEnumerable<T> items, T findingItem)
    {
        var index = -1;
        foreach (var item in items)
        {
            ++index;
            if (object.Equals(item, findingItem))
                return index;
        }
        return index;
    }

    public static string To1CString(this double d)
    {
        return d.ToString("N1");
    }

    public static string To1CString(this float f)
    {
        return f.ToString("N1");
    }

    public static IEnumerable<IEnumerable<T>> SplitByCount<T>(this IEnumerable<T> items, int countLimit)
    {
        return items.SplitByLimit(countLimit, x => 1);
    }
    public static IEnumerable<IEnumerable<T>> SplitByLimit<T>(this IEnumerable<T> items, int lengthLimit, Func<T, int> getLength)
    {
        var result = new List<T>();
        var totalLength = 0;
        foreach (var item in items)
        {
            var length = getLength(item);
            if (totalLength + length > lengthLimit && result.Any())
            {
                yield return result.ToArray();
                result.Clear();
                totalLength = 0;
            }
            totalLength += length;
            result.Add(item);
        }
        if (result.Any())
            yield return result;
    }
    public static IEnumerable<IEnumerable<T>> SplitByEqualLimit<T>(this IEnumerable<T> items, int lengthLimit, Func<T, int> getLength)
    {
        var result = new List<T>();
        var maxLength = 0;
        foreach (var item in items)
        {
            var length = getLength(item);
            if (maxLength < length)
                maxLength = length;
            if (maxLength * (result.Count + 1) > lengthLimit && result.Any())
            {
                yield return result.ToArray();
                result.Clear();
                maxLength = 0;
            }
            result.Add(item);
        }
        if (result.Any())
            yield return result;
    }

    public static IEnumerable<T> SequenceElements<T>(int count, IEnumerable<T> initialPreviousValues, Func<T[], T> getNextValue)
    {
        var preriousItems = initialPreviousValues.ToList();
        for (var i = 0; i < count; ++i)
        {
            var value = getNextValue(preriousItems.ToArray());
            preriousItems.Add(value);
            yield return value;
        }
    }
}
public static class LogUtilities {
    private static object _lock = new object();
    public static void Log(string filePath, int maxSizeInKb, Exception exception)
    {
        Log(filePath, maxSizeInKb, exception.ToString());
    }
    public static void Log(string filePath, int maxSizeInKb, string message)
    {
        var dateTime = DateTime.Now.ToString("dd'.'MM'.'yy' 'HH':'mm':'ss");
        var logContent = $"{dateTime}{Environment.NewLine}{message}";
        lock (_lock)
        {
            if (File.Exists(filePath) && maxSizeInKb > 0 && new FileInfo(filePath).Length > maxSizeInKb * 1024)
            {
                var lines = File.ReadAllLines(filePath);
                File.WriteAllLines(filePath, lines.Skip(lines.Length / 2));
            }
            File.AppendAllText(filePath, Environment.NewLine + Environment.NewLine + logContent);
        }
    }
}
public static class RandomUtilities {
    private static readonly Random random = new Random();

    public static int GetRandomInt(int min, int max, int[] excludeValues = null)
    {
        if (min == max)
            return min;
        if (max < min)
            ConvertUtilities.Switch(ref min, ref max);
        var value = random.Next(min, max + 1);
        if (excludeValues != null && excludeValues.Contains(value))
            return GetRandomInt(max, excludeValues);
        return value;
    }
    public static int GetRandomIndex(int maxCount)
    {
        return random.Next(0, maxCount);
    }
    public static int GetRandomPositiveInt(int maxCount)
    {
        return random.Next(1, maxCount);
    }
    public static int GetRandomInt(int max, int[] excludeValues = null)
    {
        var value = random.Next(-1 * max, max + 1);
        if (excludeValues != null && excludeValues.Contains(value))
            return GetRandomInt(max, excludeValues);
        return value;
    }
    public static char GetRandomChar(this string text)
    {
        return text[GetRandomIndex(text.Length)];
    }
    public static T GetRandomValue<T>(this IReadOnlyList<T> items)
    {
        return items[GetRandomIndex(items.Count)];
    }

    public static T GetRandomEnum<T>()
    {
        var enumValues = System.Enum.GetValues(typeof(T)).Cast<int>().ToArray();
        return (T)(object)enumValues.GetRandomValue();
    }
    public static bool GenerateBool()
    {
        return GetRandomInt(1, 2) == 1;
    }

    public static IEnumerable<T> OrderByRandom<T>(this IEnumerable<T> items)
    {
        return items.OrderBy(x => GetRandomIndex(100000));
    }
}
public static class FileUtilities {
    public static void SimpleWatchFiles(this FileSystemWatcher watcher, Action action)
    {
        watcher.EnableRaisingEvents = true;
        watcher.NotifyFilter = NotifyFilters.LastWrite;
        watcher.Changed += delegate { action(); };
        watcher.Created += delegate { action(); };
        watcher.Deleted += delegate { action(); };
        watcher.Renamed += delegate { action(); };
    }
}