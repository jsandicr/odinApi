﻿using OdinApi.Models.Obj;

namespace OdinApi.Models.Data.Interfaces
{
    public interface IServiceModel
    {
        public List<Service> GetServices();
        public Service GetServiceById(int id);
        public Service PostService(Service service);
        public Service PutService(Service service);
        public Service DeleteService(int id);
        public  List<Service> GetServiceStatus(bool status);
        public List<Service> GetListSubServicioById(long id);
        public List<Service> GetFinalServices();
    }
}