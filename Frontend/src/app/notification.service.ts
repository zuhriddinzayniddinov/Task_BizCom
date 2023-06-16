import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {

  constructor() { }

  public showNotification(message : string) {
    const notification = document.createElement('div');
    notification.classList.add('notification');
    notification.textContent = message;
    document.body.appendChild(notification);
      notification.classList.add('show');

    setTimeout(() => {
      notification.remove();
    }, 5000);
  }
}
