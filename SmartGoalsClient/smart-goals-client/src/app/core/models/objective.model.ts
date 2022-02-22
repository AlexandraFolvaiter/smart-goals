export class Objective {
  id!: string;
  goalId!: string;
  name: string | undefined;
  description: string | undefined;
  timeEstimated: string | undefined;
  isFinished!: boolean;
  specific: string | undefined;
  measurable: string | undefined;
  achievable: string | undefined;
  realistic: string | undefined;
  timely: string | undefined;
}