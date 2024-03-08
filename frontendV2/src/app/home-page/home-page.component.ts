import { Component } from '@angular/core';
import { StateService } from '../state.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent {
  toTranslate?: string;
  fromLanguage?: string;
  constructor(public state: StateService) {}

  onTranlate() {
    if (!this.fromLanguage){
      this.fromLanguage = "NoLanguageSelected";
    }
    var dto = {
      eventType: "ClientWantsToTranslate",
      messageToTranslate: this.toTranslate,
      fromLanguage: this.fromLanguage,
    }
    this.state.ws.send(JSON.stringify(dto));
  }
}
