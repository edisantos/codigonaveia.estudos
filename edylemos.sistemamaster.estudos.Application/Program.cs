using edylemos.sistemamaster.estudos.Services.Interface.Questionario;
using edylemos.sistemamaster.estudos.Services.Repository;
using edylemoss.sistemamaster.estudoss.Data.Contexto;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

string strConnection = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<DataContexto>(options =>
{
    options.UseSqlServer(strConnection);
});
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IQuestionarioServices, QuestionarioRepository>();
builder.Services.AddScoped<IPerguntaServices, PerguntaRepository>();
builder.Services.AddScoped<IRespostaServices, RespostaRepository>();

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
