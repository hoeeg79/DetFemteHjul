import { Injectable } from '@angular/core';
import { BaseDto, ServerBroadcastTranslatedTextDto } from './BaseDto';

@Injectable({
  providedIn: 'root'
})
export class StateService {

  translation?: string;
  language?: string;
  ws: WebSocket = new WebSocket('ws://localhost:8181');
  constructor() {    this.ws.onmessage = message => {
    const messageFromServer = JSON.parse(message.data) as BaseDto<any>

    // @ts-ignore
    this[messageFromServer.eventType].call(this, messageFromServer);
  } }

  ServerBroadcastTranslatedText(dto: ServerBroadcastTranslatedTextDto) {
    this.translation = dto.translation;
    this.language = dto.language;
  }
}