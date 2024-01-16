export class User {
  UserId: number;
  username: string;
  password: string;
  confirmPsw: string;
  creationDate: Date;
  firstName: string;
  lastName: string;
  email: string;
  laboratoryId: number;
  token?: string;
  deleted: boolean;
  activeRoles: any[];
  userGroups: any[];
  ldapData: any;
  isLDAPUser: boolean;
}
