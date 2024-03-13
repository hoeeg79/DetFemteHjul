export class BaseDto<T> {
  eventType: string;

  constructor(init?: Partial<T>) {
    this.eventType = this.constructor.name;
    Object.assign(this, init);
  }
}

export class ServerBroadcastTranslatedTextDto extends BaseDto<ServerBroadcastTranslatedTextDto> {
  translation?: string;
  language?: string;
}

export class ServerGivesLanguagesDto extends BaseDto<ServerGivesLanguagesDto> {
  language?: string[];
  code?: string[];
}
