using AutoMapper;
using FormData.DataLayer;
using FormData.Models;
using System.Web.Mvc;

namespace FormData.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly IMapper Mapper;
        protected BaseController()
        {
            var config = new MapperConfiguration(x =>
            {
                x.CreateMap<FormData.DataLayer.Customer, CustomerEdit>();
            });
            Mapper = config.CreateMapper();
        }
    }
}