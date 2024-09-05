using System;
using API.Entities;

namespace API.Interface;

public interface ITokenServies
{
   string CreateToken(AppUser user);
}
