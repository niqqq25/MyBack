using Microsoft.EntityFrameworkCore.ValueGeneration;
using MyBack.Application.Common.Interfaces.Persistence;

namespace MyBack.Infrastructure.Persistence;

public class SequentialGuidGenerator : ISequentialGuidGenerator
{
    private readonly SequentialGuidValueGenerator _sequentialGuidValueGenerator;
    
    public SequentialGuidGenerator()
    {
        _sequentialGuidValueGenerator = new SequentialGuidValueGenerator();
    }
    
    public Guid Next()
    {
        return _sequentialGuidValueGenerator.Next(default!);
    }
}