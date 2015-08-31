using ScalabelSimpleBlog.Business.Mappings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScalabelSimpleBlog.App_Start
{
    public static  class AutomapperConfig
    {
        public static void Configure()
        {
            AutoMapper.Mapper.AddProfile<ArticlesProfiles>();
            AutoMapper.Mapper.AddProfile<TagsProfiles>();
        }
    }
}