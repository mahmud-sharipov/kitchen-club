global using KitchenClube.Data;
global using KitchenClube.Exceptions;
global using KitchenClube.Models;
global using KitchenClube.Requests.Food;
global using KitchenClube.Requests.Menu;
global using KitchenClube.Requests.MenuItem;
global using KitchenClube.Requests.User;
global using KitchenClube.Requests.UserMenuItemSelection;
global using KitchenClube.Responses;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.EntityFrameworkCore;
global using System.Collections.ObjectModel;
global using System.Net;

global using Microsoft.IdentityModel.Tokens;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Text;
global using KitchenClube.MiddleWare;
global using Microsoft.OpenApi.Models;
global using Microsoft.AspNetCore.Authorization;

global using KitchenClube.Requests.Roles;
global using AutoMapper;

global using FluentValidation;

global using Microsoft.AspNetCore.Identity;
global using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

global using KitchenClube.Filters;
global using Microsoft.AspNetCore.Mvc.Filters;

global using KitchenClube.Extentions;
global using KitchenClube.Configurations;