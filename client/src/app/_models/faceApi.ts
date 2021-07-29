import { FaceItemDto } from "./Attributes";

export interface FaceApi {
    imageUrl: string;
    items: FaceItemDto[];
}