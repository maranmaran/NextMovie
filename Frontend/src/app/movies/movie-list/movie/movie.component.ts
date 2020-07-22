import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { MediaObserver } from '@angular/flex-layout';
import { environment } from 'src/environments/environment';
import { Movie } from 'src/models/movie.model';

@Component({
  selector: 'app-movie',
  templateUrl: './movie.component.html',
  styleUrls: ['./movie.component.scss']
})
export class MovieComponent implements OnInit {

  @Input() movie: Movie;
  @Input() userWatched: boolean;
  @Input() userLiked: boolean;
  @Input() userDisliked: boolean;

  @Output() like = new EventEmitter<Movie>();
  @Output() dislike = new EventEmitter<Movie>();

  imageUrl = environment.tmdbImagesUrl;

  constructor(
    public media: MediaObserver
  ) { }

  ngOnInit(): void {
  }

}
