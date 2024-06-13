using Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;


public class TopicController(IServiceManager _serviceManager) : ApiController(_serviceManager)
{
    [HttpGet]
    public async Task<IActionResult> GetTopics()
    {
        var topics = await _serviceManager.TopicService.GetTopics();

        return Ok(topics);
    }
    [Authorize(Roles = "User")]
    [HttpGet("with-content")]
    public async Task<IActionResult> GetTopicsWithContent()
    {
        var topics = await _serviceManager.TopicService.GetTopicsWithContent();

        return Ok(topics);
    }
    [Authorize(Roles = "User")]
    [HttpPost("add-topic")]
    public async Task<IActionResult> AddTopic([FromBody] CreateTopicDto createTopicDto) 
    {
        var user = await _serviceManager.UserService.GetUserWithClaim(User);
        await _serviceManager.TopicService.CreateTopic(user.Id,createTopicDto);

        return Ok("Topic add Successfully");
    }


    [Authorize(Roles = "User")]
    [HttpPut("update-topic/{topicId}")]
    public async Task<IActionResult> UpdateTopic([FromBody] UpdateTopicDto updateTopicDto, int topicId)
    {
        

        await _serviceManager.TopicService.UpdateTopic(topicId, updateTopicDto);

        return Ok("Topic Updated Successfully");
    }
    [Authorize(Roles = "User")]
    [HttpDelete("delete-topic/{topicId}")]
    public async Task<IActionResult> DeleteTopic(int topicId)
    {

        await _serviceManager.TopicService.DeleteTopic(topicId);

        return Ok("Topic Deleted Successfully");
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("change-topic-state/{topicId}/{state}")]
    public async Task<IActionResult> ChangeTopicState(int topicId, State state)
    {

        await _serviceManager.TopicService.ChangeTopicState(topicId, state);
        return Ok($"Topic state updated to: {state.ToString()}");
    }
    [Authorize(Roles = "Admin")]
    [HttpPut("change-topic-status/{topicId}/{status}")]
    public async Task<IActionResult> ChangeTopicStatus(int topicId, Status status) 
    {

        await _serviceManager.TopicService.ChangeTopicStatus(topicId, status);
        return Ok($"Topic status updated to: {status.ToString()}");
    }

}
