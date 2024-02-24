using FluentValidation;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RenoCare.Core.Base
{
    /// <summary>
    /// Represents a pipeline behavior to surround the inner handler.
    /// </summary>
    /// <typeparam name="TRequest">The mediator request.</typeparam>
    /// <typeparam name="TResponse">The expected response for the request.</typeparam>
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        #region Fields

        private readonly IEnumerable<IValidator<TRequest>> _validators;

        #endregion

        #region Ctor

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Handle the pipeline behavior to validate the request.
        /// </summary>
        /// <param name="request">The mediator request.</param>
        /// <param name="next">The next task to execute in the pipeline.</param>
        /// <param name="cancellationToken">Used to cancel operations.</param>
        /// <returns>
        /// A task represents the asynchronous operation,
        /// the task result contains the response.
        /// </returns>
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators.Any())
            {
                var validationContext = new ValidationContext<TRequest>(request);

                var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(validationContext, cancellationToken)));

                var failures = validationResults.SelectMany(r => r.Errors).Where(e => e != null).ToList();

                if (failures.Any())
                {
                    throw new ValidationException(failures.Select(x => x.PropertyName + " : " + x.ErrorMessage).FirstOrDefault(), failures);
                }
            }

            return await next();
        }

        #endregion
    }
}
