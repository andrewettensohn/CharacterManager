using CharacterManager.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CharacterManager.Services
{
    public class ServiceBase
    {
        protected readonly IRepository Repository;

        protected ServiceBase(IRepository repository)
        {
            Repository = repository;
        }
    }
}
