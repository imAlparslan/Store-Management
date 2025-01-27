﻿using CatalogManagement.Domain.Common.Interfaces;

namespace CatalogManagement.Domain.ProductAggregate.Events;
public sealed record ProductCreatedDomainEvent(Product Product) : IDomainEvent;
