// <copyright file="Program.cs" company="Skd">
// Copyright (c) Skd. All rights reserved.
// </copyright>

#pragma warning disable SA1200
using FisherYates.Services;
using FisherYates.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IFisherYatesShufflerService, FisherYatesShufflerService>();

builder.Services.AddMvc();
builder.Services.AddSwaggerGen();

var app = builder.Build();
app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();
app.MapControllers();
app.Run();