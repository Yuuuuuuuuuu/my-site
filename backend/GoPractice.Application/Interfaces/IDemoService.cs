using GoPractice.Application.Dtos.Demo;

namespace GoPractice.Application.Interfaces;

public interface IDemoService : ICrudAppService<DemoDto, DemoCreateRequest, DemoUpdateRequest>
{
}
