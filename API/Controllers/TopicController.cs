﻿using Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace API.Controllers;


public class TopicController(IServiceManager _serviceManager) : ApiController(_serviceManager)
{
    [HttpGet]
    public async Task<IActionResult> GetTopics()
    {
        var topics = await _serviceManager.TopicService.GetTopics();

        _response = new ApiResponse("Topics", true, topics, Convert.ToInt32(HttpStatusCode.OK));
        return StatusCode(_response.StatusCode, _response);
    }
    //[Authorize(Roles = "User")]
    [HttpGet("with-content/{pageNum}")]
    public async Task<IActionResult> GetTopicsWithContent(int pageNum)
    {
        var topics = await _serviceManager.TopicService.GetTopicsWithContent(pageNum);
        
        _response = new ApiResponse("Topics with Content", true, topics, Convert.ToInt32(HttpStatusCode.OK));
        return StatusCode(_response.StatusCode, _response); 
    }
    [Authorize(Roles = "User")]
    [HttpPost("create-topic")]
    public async Task<IActionResult> AddTopic([FromBody] CreateTopicDto createTopicDto) 
    {
        var user = await _serviceManager.UserService.GetUserWithClaim(User);
        await _serviceManager.TopicService.CreateTopic(user.Id,createTopicDto);

        _response = new ApiResponse("Topic add Successfully", true, null, Convert.ToInt32(HttpStatusCode.Created));
        return StatusCode(_response.StatusCode, _response);
    }


    [Authorize(Roles = "User")]
    [HttpPut("update-topic/{topicId}")]
    public async Task<IActionResult> UpdateTopic([FromBody] UpdateTopicDto updateTopicDto, int topicId)
    {
        

        await _serviceManager.TopicService.UpdateTopic(topicId, updateTopicDto);
        _response = new ApiResponse("Topic Updated Successfully", true, null, Convert.ToInt32(HttpStatusCode.OK));
        return StatusCode(_response.StatusCode, _response);
    }
    [Authorize(Roles = "User")]
    [HttpDelete("delete-topic/{topicId}")]
    public async Task<IActionResult> DeleteTopic(int topicId)
    {

        await _serviceManager.TopicService.DeleteTopic(topicId);
        _response = new ApiResponse("Topic Deleted Successfully", true, null, Convert.ToInt32(HttpStatusCode.OK));
        return StatusCode(_response.StatusCode, _response);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("change-topic-state/{topicId}/{state}")]
    public async Task<IActionResult> ChangeTopicState(int topicId, State state)
    {
        await _serviceManager.TopicService.ChangeTopicState(topicId, state);

        _response = new ApiResponse($"Topic state updated to: {state.ToString()}", true, null, Convert.ToInt32(HttpStatusCode.OK));
        return StatusCode(_response.StatusCode, _response);
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("change-topic-status/{topicId}/{status}")]
    public async Task<IActionResult> ChangeTopicStatus(int topicId, Status status) 
    {
        await _serviceManager.TopicService.ChangeTopicStatus(topicId, status);
        _response = new ApiResponse($"Topic status updated to: {status.ToString()}", true, null, Convert.ToInt32(HttpStatusCode.OK));
        return StatusCode(_response.StatusCode, _response);
    }

}
