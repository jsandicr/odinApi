﻿using OdinApi.Models.Data.Interfaces;
using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Classes
{
    public class ServiceModel : IServiceModel
    {

        private readonly OdinContext _context;

        public ServiceModel(OdinContext context)
        {
            _context = context;
        }
        public Service GetServiceById(int id)
        {
            try
            {
                Service service = _context.Service.Find(id);
                if (service != null)
                {
                    return service;
                }
                else
                {
                    return new Service();
                }
            }
            catch (Exception)
            {
                return new Service();
            }
        }

        public List<Service> GetServices()
        {
            try
            {
                return _context.Service.ToList();
            }
            catch (Exception)
            {
                return new List<Service>();
            }
        }

        public Service PostService(Service service)
        {
            try
            {
                _context.Service.Add(service);
                _context.SaveChanges();
                return service;
            }
            catch (Exception)
            {
                return new Service();
            }
        }

        public Service DeleteService(int id)
        {
            try
            {
                Service service = _context.Service.Find(id);
                if (service != null)
                {
                    //_context.Remove(service);

                    if (service.active)
                    {
                        service.active = false;
                    }
                    else { 
                        service.active = true;
                    }
                    _context.Update(service);
                    _context.SaveChanges();
                    return service;
                }
                else
                {
                    return new Service();
                }
            }
            catch (Exception)
            {
                return new Service();
            }
        }

        public Service PutService(Service service)
        {
            try
            {
                _context.Update(service);
                _context.SaveChanges();
                return service;
            }
            catch (Exception)
            {
                return new Service();
            }
        }

        public  List<Service> GetServiceStatus(bool status)
        {
            try
            {
                return _context.Service.Where(u => u.active == status && u.serviceMain==null).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<Service> GetListSubServicioById(long id)
        {
            try
            {
                return _context.Service.Where(u => u.active == true && u.idServiceMain == id).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
