﻿using MediatR;

namespace MiniECommerce.Application.Features.Queries.Basket.GetBasketItems
{
    public class GetBasketItemsQueryRequest :IRequest<List<GetBasketItemsQueryResponse>>
    {
    }
}