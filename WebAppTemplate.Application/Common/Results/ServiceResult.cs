using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAppTemplate.Domain.Enums;

namespace WebAppTemplate.Application.Common.Results
{
    public abstract record ServiceResult<TEntity>
    {
        public sealed record Success(TEntity Entity)
         : ServiceResult<TEntity>;
           
        public sealed record Failure(
                               IReadOnlyList<string> Errors,
                               ErrorType ErrorType)
                               : ServiceResult<TEntity>;

        public static ServiceResult<TEntity> FromSuccess(TEntity entity)
       => new Success(entity);

        public static ServiceResult<TEntity> FromFailure(string[] errors, ErrorType errorType)
            => new Failure(errors, errorType);

    }

}

    

