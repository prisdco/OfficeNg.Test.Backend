using FullStack.ServiceModel;
using FuulStack_Test.Service;
using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FullStack.Application.UseCase.Queries
{
    public class GetMoviesQuery : IRequest<ResultViewModel>
    {
        private string Title { get; set; }
        private string Id { get; set; }
        private int PageNumber { get; set; } = 1;

        public GetMoviesQuery(string title, string id, int PageNum)
        {
            Id = id;
            Title = title;
            PageNumber = PageNum;

        }

        public class GetMoviesQueryCommandHandler : IRequestHandler<GetMoviesQuery, ResultViewModel>
        {
            private readonly IApplicationClientFactory clientFactory;
            private readonly IHttpContextAccessor httpContextAccessor;
            public GetMoviesQueryCommandHandler(IHttpContextAccessor httpContextAccessor, IApplicationClientFactory clientFactory)
            {
                this.clientFactory = clientFactory;
                this.httpContextAccessor = httpContextAccessor;
            }


            public async Task<ResultViewModel> Handle(GetMoviesQuery model, CancellationToken cancellationToken)
            {
                if (string.IsNullOrEmpty(model.Id))
                {
                    var result = await clientFactory.GetMovies(model.Title, model.PageNumber);
                    return ResultViewModel.Ok(result);
                }
                else if(string.IsNullOrEmpty(model.Title))
                {
                    var result = await clientFactory.GetMovie(model.Id);
                    return ResultViewModel.Ok(result);
                }
                else
                {
                    return ResultViewModel.Fail("Title and Id cannot contain value in the same request");
                }

            }
        }
    }
}
