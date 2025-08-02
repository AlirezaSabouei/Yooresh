using Microsoft.Playwright;
using NUnit.Framework;
using System.Diagnostics;

namespace Yooresh.Village.Tests.Common;

public abstract class TestBase
{
    private Process _blazorProcess;
    private Process _apiProcess;
    private IPlaywright _playwright;
    private IBrowser _browser;
    private IBrowserContext _context;
    protected IPage Page;

    protected virtual void StartBlazorProcess()
    {
        _blazorProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "run --project ../../../../../src/Yooresh.Village",
                WorkingDirectory = Directory.GetCurrentDirectory(),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = false
            }
        };
        _blazorProcess.Start();
        Thread.Sleep(500);
    }

    protected virtual void StartApiProcess()
    {
        _apiProcess = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = "dotnet",
                Arguments = "run --project ../../../../../src/Yooresh.API",
                WorkingDirectory = Directory.GetCurrentDirectory(),
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = false
            }
        };
        _apiProcess.Start();
        Thread.Sleep(500);
    }

    [OneTimeSetUp]
    public async Task InitializeAsync()
    {
        _apiProcess?.Kill();
        _blazorProcess?.Kill();

        StartApiProcess();
        StartBlazorProcess();

        // Uncomment the following code to debug the process problems

        _blazorProcess.OutputDataReceived += (sender, args) =>
        {
            if (!string.IsNullOrEmpty(args.Data))
                Console.WriteLine("[Blazor STDOUT] " + args.Data);
        };
        _blazorProcess.ErrorDataReceived += (sender, args) =>
        {
            if (!string.IsNullOrEmpty(args.Data))
                Console.Error.WriteLine("[Blazor STDERR] " + args.Data);
        };
        _blazorProcess.BeginOutputReadLine();
        _blazorProcess.BeginErrorReadLine();

        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new() { Headless = false });
        _context = await _browser.NewContextAsync();
        Page = await _browser.NewPageAsync();

    }

    [OneTimeTearDown]
    public async Task DisposeMainAsync()
    {
        _apiProcess?.Kill();
        _blazorProcess?.Kill();
        await _browser.CloseAsync();
        _playwright.Dispose();
    }
}
