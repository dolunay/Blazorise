﻿using System;
using System.Threading.Tasks;
using AngleSharp.Dom;
using Bunit;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;

namespace Blazorise.Tests.Extensions;

internal static class TestExtensions
{
    public static TimeSpan WaitTime = TimeSpan.FromSeconds( 5 );

    public static IServiceCollection AddTestData( this IServiceCollection services )
    {
        services.AddMemoryCache();
        services.AddScoped<Blazorise.Shared.Data.EmployeeData>();
        services.AddScoped<Blazorise.Shared.Data.CountryData>();
        return services;
    }

    public static async Task Click<T>( this IRenderedComponent<T> comp, string selector ) where T : IComponent
    {
        await comp.WaitForElement( selector, WaitTime ).ClickAsync();
    }

    public static async Task Input<T, TInput>( this IRenderedComponent<T> comp, string selector, TInput value, Action<IElement> action = null ) where T : IComponent
    {
        var element = comp.WaitForElement( selector, WaitTime );
        if ( action is not null )
            action( element );
        await element.InputAsync( value?.ToString() );
    }

    public static Task ClickOnAsync<T>( this IRenderedComponent<T> cut, string selector, MouseEventArgs mouseEventArgs = null ) where T : IComponent
    {
        var element = cut.WaitForElement( selector, WaitTime );
        return element.ClickAsync( mouseEventArgs ?? new() );
    }
}