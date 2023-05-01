using CI_Platform.Entities.DataModels;
using CI_Platform.Repository.Interface;
using CI_Platform.Repository.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CIdbcontext>();
builder.Services.AddScoped<IRegister, Register>();
builder.Services.AddScoped<ILanding, Landing>();
builder.Services.AddScoped<IVolunteer, Volunteer>();
builder.Services.AddScoped<IStoryListing, StoryListing>();
builder.Services.AddScoped<IUserprofile, Userprofile>();
builder.Services.AddScoped<IAdmins,Admins>();
builder.Services.AddSession();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthorization();
app.MapAreaControllerRoute(
            name: "MyAreaProducts",
            areaName: "Admin",
            pattern: "Admin/{controller=Admin}/{action=Users}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");
app.Run();
