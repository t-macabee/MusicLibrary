import {User} from "./user";

export class UserParams {
  pageNumber = 1;
  pageSize = 5;
  gender: string;

  constructor(user: User) {
    this.gender = user.gender === 'female' ? 'male' : 'female';
  }

}
