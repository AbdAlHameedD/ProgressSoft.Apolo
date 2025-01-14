import { OperationStatus } from "../enums/OperationStatus";

export class Result<Type> {
    public status: OperationStatus = OperationStatus.Failed;
    public data?: Type;
    public message?: string;
}