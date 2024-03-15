import { Injectable } from '@angular/core';
import {BaseDto, ServerBroadcastTranslatedTextDto, ServerGivesLanguagesDto} from './BaseDto';

@Injectable({
  providedIn: 'root'
})
export class StateService {

  translation?: string;
  language?: string;
  languages?: string[];
  fromLanguagesTemp: string[] = ["Detect Language"];
  fromLanguages?: string[];
  code?: string[];
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

  ServerGivesLanguages(dto: ServerGivesLanguagesDto) {
    this.languages = dto.language;
    this.fromLanguages = this.fromLanguagesTemp!.concat(dto.language!);
    this.code = dto.code;
  }
}
