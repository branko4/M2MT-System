using M2MT.Shared.IRepository;
using M2MT.Shared.Model;

namespace M2MT.Test.Shared.Util
{
    public class GenericFakes
    {
        public static MethodConfiguration<Repo>[] GetReadMethodConfiguration<Repo, Model>(IEnumerable<Model> models) where Repo : IReadRepository<Model> where Model : Base
        {
            return new[] {
                    BuildConfigForGetAllOf<Repo, Model>(models),
                    BuildConfigForExcistsOf<Repo, Model>(TestModel.BaseToGuid(models)),
                    BuildConfigForGetOneOf<Repo, Model>(models),
            };
        }

        public static MethodConfiguration<Repo>[] GetCRUDMethodConfiguration<Repo, Model>(List<Model> models, Guid createID) where Repo : ICRUDRepository<Model> where Model : Base
        {
            var confs = new List<MethodConfiguration<Repo>>();
            confs.AddRange(
                new[] { 
                    BuildConfigForCreateOf<Repo, Model>(models, createID),
                    BuildConfigForRemoveOf<Repo, Model>(models),
                }
            );
            confs.AddRange(GetReadMethodConfiguration<Repo, Model>(models).ToList());
            return confs.ToArray();
        }

        public static MethodConfiguration<repo> BuildConfigForExcistsOf<repo, T>(IEnumerable<Guid> knowRefs) where repo : IReadRepository<T>
        {
            return new MethodConfiguration<repo>((repoFake) =>
            {
                return A.CallTo(() => repoFake.Excists(A<Guid>.Ignored)).ReturnsLazily(x => Task.Run(async () => {
                    await Task.Delay(10);
                    var check = knowRefs.Contains(x.GetArgument<Guid>(0));
                    return check;
                }));
            });
        }

        public static MethodConfiguration<repo> BuildConfigForGetOneOf<repo, T>(IEnumerable<T> knowRefs) where repo : IReadRepository<T> where T : Base
        {
            return new MethodConfiguration<repo>((repoFake) =>
            {
                return A.CallTo(() => repoFake.GetOne(A<Guid>.Ignored)).ReturnsLazily(x => Task.Run(async () => {
                    await Task.Delay(10);
                    var id = x.GetArgument<Guid>(0);
                    return knowRefs.Where(r => r.ID == id).FirstOrDefault();
                }));
            });
        }

        public static MethodConfiguration<repo> BuildConfigForGetAllOf<repo, T>(IEnumerable<T> knowRefs) where repo : IReadRepository<T> where T : Base
        {
            return new MethodConfiguration<repo>((repoFake) =>
            {
                return A.CallTo(() => repoFake.GetAll()).Returns(knowRefs);
            });
        }

        public static MethodConfiguration<Repo> BuildConfigForCreateOf<Repo, T>(List<T> models, Guid ID) where Repo : ICRUDRepository<T> where T : Base
        {
            return new MethodConfiguration<Repo>((repoFake) =>
            {
                return A.CallTo(() => repoFake.Create(A<T>.Ignored)).ReturnsLazily(x =>
                Task.Run(async () =>
                {
                    await Task.Delay(10);
                    var returnValue = x.GetArgument<T>(0);
                    if (!Guid.Empty.Equals(returnValue.ID)) returnValue.ID = ID;
                    models.Add(returnValue);
                    return returnValue;
                }));
            });
        }

        public static MethodConfiguration<Repo> BuildConfigForRemoveOf<Repo, T>(List<T> models) where Repo : ICRUDRepository<T> where T : Base
        {
            return new MethodConfiguration<Repo>((repoFake) =>
            {
                return A.CallTo(() => repoFake.Remove(A<Guid>.Ignored)).ReturnsLazily(x =>
                Task.Run(async () =>
                {
                    await Task.Delay(10);
                    var returnValue = x.GetArgument<Guid>(0);
                    return models.Where(m => m.ID.Equals(returnValue)).FirstOrDefault();
                }));
            });
        }
    }
}
