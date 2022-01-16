using System;
using System.IO;
using System.Net;
using System.Net.Mime;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using FileStore.Models;

namespace FileStore.Controllers;

class FileDirectory {
  public static string Path = "/tmp";

  public static string[] GetFiles() {
    return Directory.GetFiles(Path);
  }
}

[ApiController]
[Route("[controller]")]
public class FileSaveController : ControllerBase
{
  [HttpPost]
  public async Task<IActionResult> Post([FromForm] FileModel fileModel)
  {
    IFormFile? fileIn = fileModel.FormFile;
    try {
      IFormFile file = fileIn == null ? throw new NullReferenceException("missing file") : fileIn;
      Console.WriteLine("received file: " + file.FileName);
      string filePath = Path.Combine(FileDirectory.Path, file.FileName);
      using (Stream fileStream = new FileStream(filePath, FileMode.Create)) {
        await file.CopyToAsync(fileStream);
      }
      return StatusCode(StatusCodes.Status201Created);
    } catch (Exception e) {
      Console.Error.WriteLine(e);
      return StatusCode(StatusCodes.Status500InternalServerError);
    }
  }
}

[ApiController]
[Route("[controller]")]
public class FileListController : ControllerBase
{
  [HttpGet]
  public FileListModel Get()
  {
    var files = FileDirectory
      .GetFiles()
      .ToArray()
      .Select(Path.GetFileName)
      .ToList();
    Console.WriteLine("return file list");
    return new FileListModel{FileNames = files.ToList()};
  }
}

[ApiController]
[Route("[controller]")]
public class FileDownloadController : ControllerBase
{
  [HttpGet("{id}")]
  public FileResult Get(string id)
  {
    Console.WriteLine("download request for " + id);

    var path = Path.Combine(FileDirectory.Path, id);
    var bytes = System.IO.File.ReadAllBytes(path);

    return File(bytes, MediaTypeNames.Application.Octet, id);
  }
}
