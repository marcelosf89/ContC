using FluentNHibernate.Conventions;
using FluentNHibernate.Conventions.Instances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContC.Extension.EA.Repositories.Mapping.Conventions
{
    public class ClasseComumConvencao : IIdConvention, IClassConvention, IReferenceConvention
    {
        public void Apply(IIdentityInstance instance)
        {
        }

        public void Apply(IClassInstance instance)
        {
        }

        public void Apply(IManyToOneInstance instance)
        {
        }
    }
}