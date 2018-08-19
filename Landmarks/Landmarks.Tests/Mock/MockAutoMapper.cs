using AutoMapper;
using Landmarks.Web.Common.Mapper;

namespace Landmarks.Tests.Mock
{
    public static class MockAutoMapper
    {
        static MockAutoMapper()
        {
            Mapper.Initialize(config => config.AddProfile<DomainProfile>());
        }

        public static IMapper GetMapper()
        {
            return Mapper.Instance;
        }
    }
}
