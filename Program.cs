var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();


////PTenant
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=PTenant}/{action=PTenantHomePage}/{id?}");
////   .WithStaticAssets();

//ATenant
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=ATenant}/{action=ATenantLease}/{id?}");
//   .WithStaticAssets();

////Staff
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Staff}/{action=SMaintenanceAssignment}/{id?}");

////Property Manager
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=PropertyManager}/{action=PMDashboard}/{id?}");

////Auth
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Login}/{action=Login}/{id?}")
//    .WithStaticAssets();

app.Run();

