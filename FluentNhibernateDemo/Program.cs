using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;

namespace FluentNhibernateDemo
{
    class Program
    {
        private static ISessionFactory CreateSessionFactory()
        {
            //这三个是必须的
            return Fluently.Configure()//启动配置过程
                .Database(              //数据库配置
                  MsSqlConfiguration.MsSql2008.ConnectionString(
                      @"Data Source=(local);Initial Catalog=NhibernateDemoDB;Integrated Security=True;"
                      )
                ).Mappings(m =>
                    m.FluentMappings.AddFromAssemblyOf<Program>())
                .BuildSessionFactory();//最终调用 创建实例
        }


        static void Main(string[] args)
        {
            var sessionFactory = CreateSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var c1 = new Customer()
                    {
                        Id = 2,
                        FirstName = "1",
                        LastName = "2"
                    };
                    session.Save(c1);

                   transaction.Commit();
                   
                }
            }
        }
    }
}
