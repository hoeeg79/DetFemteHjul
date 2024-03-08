import { Component } from '@angular/core';
import {HttpClient} from "@angular/common/http";

@Component({
  selector: 'app-homepage',
  templateUrl: './homepage.component.html',
  styleUrls: ['./homepage.component.css']
})
export class HomepageComponent {
  formControl: string;
  response: string;

  constructor(public http: HttpClient) { }

  onSubmitTranslation($event: MouseEvent) {
  }
}
