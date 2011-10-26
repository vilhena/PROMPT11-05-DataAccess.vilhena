using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Mod05_ChelasDAL.Mappers;
using Mod05_ChelasDAL.Metadata;
using Mod5_DomainModel;
using Mod5_DomainModel.DomainModelMappers;

namespace Mod05_ChelasDALConsole
{
    class Program
    {
        static readonly MetaDataStore Store = new MetaDataStore();
        static readonly IdentityMap Map = new IdentityMap();
        static readonly EntityHydrater Hydrater = new EntityHydrater(Store, Map);
        private const string Connection = @"Data Source=.\sqlexpress;Initial Catalog=BlogSite;Integrated Security=True";


        static void Main(string[] args)
        {

            Store.BuildMetaDataFor(typeof(Blog).Assembly);

            var insertedBlog = InsertNewBlog();

            var getTheBlog = GetNewBlog();

            getTheBlog.Name = "Gonçalo";

            var updatedBlog = UpdateNewBlog(getTheBlog);

            DeleteNewBlog(updatedBlog.Id);

        }

        private static void DeleteNewBlog(int id)
        {
            using (var conn = new SqlConnection(Connection))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    var blogMapper = new BlogMapper(conn, tran, Store, Hydrater, Map);
                    blogMapper.Delete(id);
                    tran.Commit();
                }
            }
        }



        private static Blog UpdateNewBlog(Blog newBlog)
        {
            using (var conn = new SqlConnection(Connection))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    var blogMapper = new BlogMapper(conn, tran, Store, Hydrater, Map);

                    var updated =  blogMapper.Update(newBlog);


                    tran.Commit();

                    return updated;
                }
            }
        }

        private static Blog GetNewBlog()
        {
            using (var conn = new SqlConnection(Connection))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    var blogMapper = new BlogMapper(conn, tran, Store, Hydrater, Map);

                    return blogMapper.GetById(1);
                }
            }
        }

        private static Blog InsertNewBlog()
        {
            using (var conn = new SqlConnection(Connection))
            {
                conn.Open();
                using (var tran = conn.BeginTransaction())
                {
                    var blogMapper = new BlogMapper(conn, tran, Store, Hydrater, Map);

                    var newBlog = new Blog()
                                      {
                                          Description = "teste",
                                          Name = "my blog",
                                          Owner = new User() {OwnerId = 1}
                                      };

                    var insertedBlog = blogMapper.Insert(newBlog);

                    tran.Commit();

                    return insertedBlog;
                }
            }
        }
    }
}
