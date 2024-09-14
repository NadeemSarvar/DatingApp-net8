using System;

namespace API.DTOs;

public class JwtOption
{
    public required string Issuer { get; set; }
    public required string Key { get; set; }
    public required string ExpDate { get; set; }

}
