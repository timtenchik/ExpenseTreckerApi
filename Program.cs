﻿using Microsoft.EntityFrameworkCore;
using ExpenseTreckerApi.Data;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ExpenseTreckerApiContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ExpenseTreckerApiContext") 
	?? throw new InvalidOperationException("Connection string 'ExpenseTreckerApiContext' not found.")));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
