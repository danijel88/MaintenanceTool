﻿namespace MaintenanceTool.Web.Endpoints.ProjectEndpoints;

public class GetProjectByIdRequest
{
  public const string Route = "/Projects/{ProjectId:int}";

  public int ProjectId { get; set; }

  public static string BuildRoute(int projectId)
  {
    return Route.Replace("{ProjectId:int}", projectId.ToString());
  }
}
