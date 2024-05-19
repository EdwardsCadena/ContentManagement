using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.Core.Interfaces
{
    public interface IUserService
    {
        int GetCurrentUserId();
        int GetCurrentUserRoleId();
    }
}
