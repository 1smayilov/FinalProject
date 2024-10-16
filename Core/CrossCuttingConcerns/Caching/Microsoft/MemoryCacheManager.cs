using Core.Utilities.IoC;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions; // DI ilə bağlı digər funksiyaları istifadə etmək üçün tələb olunur.

namespace Core.CrossCuttingConcerns.Caching.Microsoft
{
    // Mən bu gün Microsoftla işləyirəm sabah Redislə də işləyə bilərəm buna görə də kodları özümə uyğun şəkilə gətirirəm (Adapter Pattern)
    public class MemoryCacheManager : ICacheManager
    {
        IMemoryCache _memoryCache;
        // Burada həmişəki injectionı etmirik ki, DI zəncirindən  kənara çıxmasın (WebApi - Business - DataAccess)
        public MemoryCacheManager()
        {
            // Yəni, AddMemoryCache() ilə əvvəldən yaddaşda məlumatları saxlamaq üçün bir sahə yaradırsınız,
            // sonra isə GetService<IMemoryCache>() ilə bu sahədən məlumatları götürür və istifadə edirsiniz
            _memoryCache = ServiceTool.ServiceProvider.GetService<IMemoryCache>();
        }
        public void Add(string key, object value, int duration)
        {
            _memoryCache.Set(key, value, TimeSpan.FromMinutes(duration));   
        }

        public T Get<T>(string key)
        {
            return _memoryCache.Get<T>(key);
        }

        public object Get(string key)
        {
            return _memoryCache.Get(key);
        }

        public bool IsAdd(string key) // Ramda belə bir keş dəyəri varmı
        {
            return _memoryCache.TryGetValue(key, out _); // Ramda belə bir key var onu bilmək istəyirəm, datanı görmək istəmirəm
        }

        public void Remove(string key)
        {
            _memoryCache.Remove(key);
        }

        public void RemoveByPattern(string pattern)
        {

            dynamic cacheEntriesCollection = null;
            var cacheEntriesFieldCollectionDefinition = typeof(MemoryCache).GetField("_coherentState", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            var cacheEntriesPropertyCollectionDefinition = typeof(MemoryCache).GetProperty("EntriesCollection", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);

            if (cacheEntriesFieldCollectionDefinition != null)
            {
                var coherentStateValueCollection = cacheEntriesFieldCollectionDefinition.GetValue(_memoryCache);
                var entriesCollectionValueCollection = coherentStateValueCollection?.GetType()
                    .GetProperty(
                        "EntriesCollection",
                        System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance
                    );


                cacheEntriesCollection = entriesCollectionValueCollection.GetValue(coherentStateValueCollection);
            }


            List<ICacheEntry> cacheCollectionValues = new List<ICacheEntry>();
            foreach (var cacheItem in cacheEntriesCollection)
            {
                var cacheItemValue = cacheItem.GetType().GetProperty("Value").GetValue(cacheItem, null);
                cacheCollectionValues.Add(cacheItemValue);
            }


            var regex = new Regex(pattern, RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
            var keysToRemove = cacheCollectionValues.Where(d => regex.IsMatch(d.Key.ToString())).Select(d => d.Key).ToList();

            foreach (var key in keysToRemove)
            {
                _memoryCache.Remove(key);
            }


        }
    }
}
// Sadələşdirilmiş Təsəvvür:
// Təsəvvür edin ki, sizin bir dəftəriniz var və bu dəftərdə müxtəlif səhifələrdə məlumatlar saxlanılıb.
// RemoveByPattern metodu, bu dəftərdə müəyyən bir nümunəyə uyğun gələn bütün məlumatları tapır və həmin səhifələri çıxarıb atır.
// Məsələn, nümunə "ad" sözünə uyğundursa, "ad" sözü keçən bütün səhifələri axtarıb, onları dəftərdən çıxarıb atır.