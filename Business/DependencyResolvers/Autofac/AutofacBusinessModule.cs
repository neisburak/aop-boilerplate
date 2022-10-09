using Autofac;
using Business.Abstract;
using Business.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;

namespace Business.DependencyResolvers.Autofac;

public class AutofacBusinessModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ProductDal>().As<IProductDal>();
        builder.RegisterType<CategoryDal>().As<ICategoryDal>();
        builder.RegisterType<UserDal>().As<IUserDal>();

        builder.RegisterType<ProductManager>().As<IProductService>();
        builder.RegisterType<CategoryDal>().As<ICategoryDal>();
        builder.RegisterType<UserManager>().As<IUserService>();
    }
}