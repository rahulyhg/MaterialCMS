﻿using MaterialCMS.Entities.Documents.Media;
using MaterialCMS.Entities.Notifications;
using MaterialCMS.Services.Notifications;

namespace MaterialCMS.Events.Documents
{
    public class MediaCategoryUpdatedNotification : IOnUpdated<MediaCategory>
    {
        private readonly IDocumentModifiedUser _documentModifiedUser;
        private readonly INotificationPublisher _notificationPublisher;

        public MediaCategoryUpdatedNotification(IDocumentModifiedUser documentModifiedUser, INotificationPublisher notificationPublisher)
        {
            _documentModifiedUser = documentModifiedUser;
            _notificationPublisher = notificationPublisher;
        }

        public void Execute(OnUpdatedArgs<MediaCategory> args)
        {
            var webpage = args.Item;
            string message = string.Format("<a href=\"/Admin/MediaCategory/Edit/{1}\">{0}</a> has been updated{2}.",
                webpage.Name,
                webpage.Id, _documentModifiedUser.GetInfo());
            _notificationPublisher.PublishNotification(message, PublishType.Both, NotificationType.AdminOnly);
        }
    }
}