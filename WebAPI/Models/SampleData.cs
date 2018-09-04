using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using Unity;
using Unity.Exceptions;

namespace WebAPI.Models
{
    public class SampleData
    {
        public Guid ID { get; set; }
        public int ApplicationId { get; set; }
        public string Type { get; set; }
        public string Summary { get; set; }
        public Double Amount { get; set; }
        public DateTime PostingDate { get; set; }
        public bool IsCleared { get; set; }
        public DateTime ClearedDate { get; set; }
    }

    public interface ISampleRepository
    {
        IEnumerable<SampleData> GetAll();
        SampleData GetById(Guid Id);
        void Add(SampleData product);

        void Update(Guid id, SampleData sampleData);
        void Delete(Guid Id);
    }


    public class SampleRepository : ISampleRepository
    {

        List<SampleData> sample;
        SampleRepository()
        {
            sample = new List<SampleData> { new SampleData {
                ID = Guid.Parse("3f2b12b8-2a06-45b4-b057-45949279b4e5"),
                ApplicationId = 197104,
                Type = "Debit",
                Summary = "Payment",
                Amount = 58.26,
                PostingDate = Convert.ToDateTime("2016-07-01T00:00:00"),
                IsCleared = true,
                 ClearedDate= Convert.ToDateTime("2016-07-01T00:00:00")
                },
                new SampleData {
                ID = Guid.Parse("3f2b12b8-2a06-45b4-b057-45949279b4e5"),
                ApplicationId = 197104,
                Type = "Debit",
                Summary = "Payment",
                Amount = 58.26,
                PostingDate = Convert.ToDateTime("2016-07-01T00:00:00"),
                IsCleared = true,
                 ClearedDate= Convert.ToDateTime("2016-07-01T00:00:00")
                }
            };
        }
        public IEnumerable<SampleData> GetAll()
        {
            return sample;
        }

        public SampleData GetById(Guid Id)
        {
            return sample.Where(m => m.ID == Id).FirstOrDefault();
            {

            }
        }

        public void Add(SampleData sampleData)
        {
            sample.Add(sampleData);

        }

        public void Update(Guid id, SampleData sampleData)
        {
            var item = sample.Where(m => m.ID == id).FirstOrDefault();
            sample.Remove(item);
            sample.Remove(sampleData);

        }

        public void Delete(Guid Id)
        {
            var item = sample.Where(m => m.ID == Id).FirstOrDefault();
            sample.Remove(item);
        }

    }


    public class UnityResolver : IDependencyResolver
    {
        private readonly IUnityContainer _container;

        public UnityResolver(IUnityContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            try
            {
                return _container.Resolve(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return null;
            }
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            try
            {
                return _container.ResolveAll(serviceType);
            }
            catch (ResolutionFailedException)
            {
                return new List<object>();
            }
        }

        public IDependencyScope BeginScope()
        {
            var child = _container.CreateChildContainer();
            return new UnityResolver(child);
        }

        public void Dispose()
        {
            _container.Dispose();
        }
    }

}