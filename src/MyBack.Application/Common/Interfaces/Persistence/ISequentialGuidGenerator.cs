namespace MyBack.Application.Common.Interfaces.Persistence;

public interface ISequentialGuidGenerator
{
    Guid Next();
}