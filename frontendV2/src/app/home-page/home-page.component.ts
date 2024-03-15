import { Component } from '@angular/core';
import { StateService } from '../state.service';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrl: './home-page.component.css'
})
export class HomePageComponent {
  toTranslate?: string;
  chosenLanguage?: string = "Afrikaans";
  fromLanguage?: string;
  fromLanguageCode?: string;
  languageCode?: string;
  constructor(public state: StateService) {}

  onTranlate() {
    this.findLanguageCode()
    var dto = {
      eventType: "ClientWantsToTranslate",
      messageToTranslate: this.toTranslate,
      toLanguage: this.languageCode,
      fromLanguage: this.fromLanguageCode,
    }
    this.state.ws.send(JSON.stringify(dto));
  }
  findLanguageCode(){
    for (let i = 0; i < this.state.languages!.length; i++) {
      if (this.state.languages![i] === this.chosenLanguage){
        this.languageCode = this.state.code![i];
        break
      }
    }
    for (let i = 0; i < this.state.fromLanguages!.length; i++) {
      if (this.state.fromLanguages![i] === this.fromLanguage){
        this.fromLanguageCode = this.state.code![i];
        break
      }
    }
  }
}
