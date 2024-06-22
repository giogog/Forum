using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SharpCompress.Common;

namespace Infrastructure.Configuration;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.HasKey(c => c.Id);

        // Configure self-referencing relationship
        builder.HasOne(c => c.ParentComment)
            .WithMany(c => c.Replies)
            .HasForeignKey(c => c.ParentCommentId)
            .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete

        // Configure Content property
        builder.Property(c => c.Body)
            .IsRequired()
            .HasMaxLength(1000);

        // Configure Created property
        builder.Property(c => c.Created)
            .IsRequired();



        // Optional: Add indexes for performance
        builder.HasIndex(c => c.TopicId);
        builder.HasIndex(c => c.ParentCommentId);
        builder.HasIndex(c => c.UserId);
    }
}
